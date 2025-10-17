import { NextRequest, NextResponse } from 'next/server'
import { prisma } from '@/lib/prisma'

export async function GET(request: NextRequest) {
  try {
    const { searchParams } = new URL(request.url)
    const page = parseInt(searchParams.get('page') || '1')
    const limit = parseInt(searchParams.get('limit') || '10')
    const category = searchParams.get('category')
    const level = searchParams.get('level')
    const search = searchParams.get('search')

    const skip = (page - 1) * limit

    const where: any = {
      isPublished: true
    }

    if (category) {
      where.category = category
    }

    if (level) {
      where.level = level
    }

    if (search) {
      where.OR = [
        { title: { contains: search, mode: 'insensitive' } },
        { description: { contains: search, mode: 'insensitive' } }
      ]
    }

    const [courses, total] = await Promise.all([
      prisma.course.findMany({
        where,
        skip,
        take: limit,
        include: {
          instructor: {
            select: {
              id: true,
              name: true,
              avatar: true
            }
          },
          _count: {
            select: {
              enrollments: true,
              reviews: true
            }
          },
          reviews: {
            select: {
              rating: true
            }
          }
        },
        orderBy: {
          createdAt: 'desc'
        }
      }),
      prisma.course.count({ where })
    ])

    const coursesWithAvgRating = courses.map(course => {
      const avgRating = course.reviews.length > 0 
        ? course.reviews.reduce((sum, review) => sum + review.rating, 0) / course.reviews.length
        : 0

      return {
        ...course,
        avgRating: Math.round(avgRating * 10) / 10,
        reviews: undefined
      }
    })

    return NextResponse.json({
      courses: coursesWithAvgRating,
      pagination: {
        page,
        limit,
        total,
        pages: Math.ceil(total / limit)
      }
    })
  } catch (error) {
    console.error('Courses fetch error:', error)
    return NextResponse.json(
      { error: 'Възникна грешка при зареждането на курсовете' },
      { status: 500 }
    )
  }
}

export async function POST(request: NextRequest) {
  try {
    const body = await request.json()
    const { title, description, price, originalPrice, category, level, instructorId } = body

    if (!title || !description || !price || !category || !instructorId) {
      return NextResponse.json(
        { error: 'Всички полета са задължителни' },
        { status: 400 }
      )
    }

    const course = await prisma.course.create({
      data: {
        title,
        description,
        price: parseFloat(price),
        originalPrice: originalPrice ? parseFloat(originalPrice) : null,
        category,
        level: level || 'BEGINNER',
        instructorId,
        isPublished: false
      },
      include: {
        instructor: {
          select: {
            id: true,
            name: true,
            avatar: true
          }
        }
      }
    })

    return NextResponse.json(
      { message: 'Курсът беше създаден успешно', course },
      { status: 201 }
    )
  } catch (error) {
    console.error('Course creation error:', error)
    return NextResponse.json(
      { error: 'Възникна грешка при създаването на курса' },
      { status: 500 }
    )
  }
}
