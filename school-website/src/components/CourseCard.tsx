import { Star, Clock, Users, ArrowRight } from 'lucide-react'

interface Course {
  id: number
  title: string
  description: string
  price: number
  originalPrice: number
  rating: number
  students: number
  duration: string
  image: string
}

interface CourseCardProps {
  course: Course
}

export default function CourseCard({ course }: CourseCardProps) {
  const discountPercentage = Math.round(((course.originalPrice - course.price) / course.originalPrice) * 100)

  return (
    <div className="card overflow-hidden group">
      {/* Course Image */}
      <div className="relative overflow-hidden">
        <img
          src={course.image}
          alt={course.title}
          className="w-full h-48 object-cover transition-transform duration-300 group-hover:scale-105"
        />
        
        {/* Discount Badge */}
        <div className="absolute top-4 left-4">
          <span className="bg-red-500 text-white px-2 py-1 rounded-full text-sm font-semibold">
            -{discountPercentage}%
          </span>
        </div>

        {/* Overlay on Hover */}
        <div className="absolute inset-0 bg-black bg-opacity-0 group-hover:bg-opacity-20 transition-all duration-300 flex items-center justify-center">
          <button className="opacity-0 group-hover:opacity-100 bg-white text-primary-600 px-4 py-2 rounded-lg font-semibold transition-all duration-300 transform translate-y-4 group-hover:translate-y-0">
            Прегледай курса
          </button>
        </div>
      </div>

      {/* Course Content */}
      <div className="p-6">
        {/* Rating and Students */}
        <div className="flex items-center justify-between mb-3">
          <div className="flex items-center">
            <div className="flex items-center">
              {[...Array(5)].map((_, i) => (
                <Star
                  key={i}
                  className={`w-4 h-4 ${
                    i < Math.floor(course.rating)
                      ? 'text-yellow-400 fill-current'
                      : 'text-gray-300'
                  }`}
                />
              ))}
            </div>
            <span className="text-sm text-gray-600 ml-2">({course.rating})</span>
          </div>
          
          <div className="flex items-center text-gray-500">
            <Users className="w-4 h-4 mr-1" />
            <span className="text-sm">{course.students}</span>
          </div>
        </div>

        {/* Course Title */}
        <h3 className="text-xl font-semibold text-gray-900 mb-2 line-clamp-2">
          {course.title}
        </h3>

        {/* Course Description */}
        <p className="text-gray-600 mb-4 line-clamp-3">
          {course.description}
        </p>

        {/* Duration */}
        <div className="flex items-center text-gray-500 mb-4">
          <Clock className="w-4 h-4 mr-2" />
          <span className="text-sm">{course.duration}</span>
        </div>

        {/* Price */}
        <div className="flex items-center justify-between mb-4">
          <div className="flex items-center space-x-2">
            <span className="text-2xl font-bold text-primary-600">
              {course.price} лв
            </span>
            <span className="text-lg text-gray-500 line-through">
              {course.originalPrice} лв
            </span>
          </div>
        </div>

        {/* CTA Button */}
        <button className="w-full btn-primary flex items-center justify-center group/btn">
          Запишете се сега
          <ArrowRight className="w-4 h-4 ml-2 group-hover/btn:translate-x-1 transition-transform" />
        </button>
      </div>
    </div>
  )
}
