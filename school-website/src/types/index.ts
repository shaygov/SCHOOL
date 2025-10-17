export interface Course {
  id: string
  title: string
  description: string
  content?: string
  price: number
  originalPrice?: number
  image?: string
  duration: number
  level: 'BEGINNER' | 'INTERMEDIATE' | 'ADVANCED'
  category: string
  isPublished: boolean
  createdAt: Date
  updatedAt: Date
  instructor: {
    id: string
    name: string
    avatar?: string
  }
  modules?: Module[]
  reviews?: Review[]
  _count?: {
    enrollments: number
    reviews: number
  }
}

export interface Module {
  id: string
  title: string
  description?: string
  order: number
  courseId: string
  lessons: Lesson[]
}

export interface Lesson {
  id: string
  title: string
  description?: string
  content?: string
  videoUrl?: string
  order: number
  duration: number
  isFree: boolean
  moduleId: string
}

export interface User {
  id: string
  email: string
  name?: string
  avatar?: string
  role: 'STUDENT' | 'INSTRUCTOR' | 'ADMIN'
  createdAt: Date
  updatedAt: Date
}

export interface Review {
  id: string
  rating: number
  comment?: string
  createdAt: Date
  user: {
    id: string
    name?: string
    avatar?: string
  }
}

export interface Enrollment {
  id: string
  userId: string
  courseId: string
  enrolledAt: Date
  completedAt?: Date
  course: Course
}

export interface Order {
  id: string
  userId: string
  amount: number
  status: 'PENDING' | 'COMPLETED' | 'FAILED' | 'REFUNDED'
  paymentId?: string
  createdAt: Date
  orderItems: OrderItem[]
}

export interface OrderItem {
  id: string
  orderId: string
  courseId: string
  price: number
  course: Course
}

export interface ApiResponse<T> {
  success: boolean
  data?: T
  error?: string
  message?: string
}

export interface PaginationParams {
  page?: number
  limit?: number
  sortBy?: string
  sortOrder?: 'asc' | 'desc'
}

export interface SearchParams {
  query?: string
  category?: string
  level?: string
  minPrice?: number
  maxPrice?: number
}
