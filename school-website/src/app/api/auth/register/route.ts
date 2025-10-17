import { NextRequest, NextResponse } from 'next/server'
import { createUser, getUserByEmail } from '@/lib/auth'
import { prisma } from '@/lib/prisma'

export async function POST(request: NextRequest) {
  try {
    const { email, password, name } = await request.json()

    if (!email || !password) {
      return NextResponse.json(
        { error: 'Email и парола са задължителни' },
        { status: 400 }
      )
    }

    const existingUser = await getUserByEmail(email)
    if (existingUser) {
      return NextResponse.json(
        { error: 'Потребител с този email вече съществува' },
        { status: 400 }
      )
    }

    const user = await createUser(email, password, name)
    
    return NextResponse.json(
      { 
        message: 'Регистрацията беше успешна',
        user: {
          id: user.id,
          email: user.email,
          name: user.name,
          role: user.role
        }
      },
      { status: 201 }
    )
  } catch (error) {
    console.error('Registration error:', error)
    return NextResponse.json(
      { error: 'Възникна грешка при регистрацията' },
      { status: 500 }
    )
  }
}
