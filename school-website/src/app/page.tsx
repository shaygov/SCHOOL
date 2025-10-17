'use client'

import { motion } from 'framer-motion'
import { ArrowRight, Star, Users, Award, CheckCircle, Play } from 'lucide-react'
import Header from '@/components/Header'
import Footer from '@/components/Footer'
import CourseCard from '@/components/CourseCard'

const fadeInUp = {
  initial: { opacity: 0, y: 60 },
  animate: { opacity: 1, y: 0 },
  transition: { duration: 0.6 }
}

const staggerChildren = {
  animate: {
    transition: {
      staggerChildren: 0.1
    }
  }
}

export default function Home() {
  const courses = [
    {
      id: 1,
      title: "Как да започнете онлайн бизнес",
      description: "Научете основите на онлайн бизнеса и как да създадете свой собствен успешен проект",
      price: 299,
      originalPrice: 599,
      rating: 4.9,
      students: 1250,
      duration: "8 часа",
      image: "/api/placeholder/400/250"
    },
    {
      id: 2,
      title: "Криптовалути и инвестиране",
      description: "Разберете света на криптовалутите и как да инвестирате разумно",
      price: 399,
      originalPrice: 799,
      rating: 4.8,
      students: 890,
      duration: "12 часа",
      image: "/api/placeholder/400/250"
    },
    {
      id: 3,
      title: "Дигитален маркетинг",
      description: "Мастер курса за дигитален маркетинг и социални мрежи",
      price: 199,
      originalPrice: 399,
      rating: 4.7,
      students: 2100,
      duration: "6 часа",
      image: "/api/placeholder/400/250"
    }
  ]

  const features = [
    {
      icon: <Award className="w-8 h-8 text-gold-500" />,
      title: "Професионално обучение",
      description: "Курсове от експерти в индустрията"
    },
    {
      icon: <Users className="w-8 h-8 text-primary-500" />,
      title: "Активна общност",
      description: "Присъединете се към хиляди успешни ученици"
    },
    {
      icon: <CheckCircle className="w-8 h-8 text-green-500" />,
      title: "Гарантиран резултат",
      description: "100% гаранция за връщане на парите"
    }
  ]

  return (
    <div className="min-h-screen">
      <Header />
      
      {/* Hero Section */}
      <section className="hero-gradient py-20 text-white">
        <div className="container mx-auto px-4">
          <motion.div
            variants={staggerChildren}
            initial="initial"
            animate="animate"
            className="text-center max-w-4xl mx-auto"
          >
            <motion.h1
              variants={fadeInUp}
              className="text-5xl md:text-6xl font-bold mb-6"
            >
              Стани <span className="text-gold-400">Богат</span> с Онлайн Обучение
            </motion.h1>
            
            <motion.p
              variants={fadeInUp}
              className="text-xl md:text-2xl mb-8 text-blue-100"
            >
              Научете тайните на успеха от най-добрите експерти в индустрията
            </motion.p>
            
            <motion.div
              variants={fadeInUp}
              className="flex flex-col sm:flex-row gap-4 justify-center items-center"
            >
              <button className="btn-gold text-lg px-8 py-4">
                Започнете сега
                <ArrowRight className="w-5 h-5 ml-2 inline" />
              </button>
              
              <button className="flex items-center text-white hover:text-gold-400 transition-colors">
                <Play className="w-6 h-6 mr-2" />
                Гледайте демо
              </button>
            </motion.div>
          </motion.div>
        </div>
      </section>

      {/* Stats Section */}
      <section className="py-16 bg-white">
        <div className="container mx-auto px-4">
          <div className="grid grid-cols-2 md:grid-cols-4 gap-8 text-center">
            <motion.div
              initial={{ opacity: 0, scale: 0.8 }}
              whileInView={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.5 }}
            >
              <div className="text-4xl font-bold text-primary-600">50K+</div>
              <div className="text-gray-600">Ученици</div>
            </motion.div>
            
            <motion.div
              initial={{ opacity: 0, scale: 0.8 }}
              whileInView={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.5, delay: 0.1 }}
            >
              <div className="text-4xl font-bold text-gold-600">100+</div>
              <div className="text-gray-600">Курса</div>
            </motion.div>
            
            <motion.div
              initial={{ opacity: 0, scale: 0.8 }}
              whileInView={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.5, delay: 0.2 }}
            >
              <div className="text-4xl font-bold text-green-600">4.9</div>
              <div className="flex justify-center items-center mt-1">
                <Star className="w-5 h-5 text-yellow-400 fill-current" />
                <span className="ml-1 text-gray-600">Рейтинг</span>
              </div>
            </motion.div>
            
            <motion.div
              initial={{ opacity: 0, scale: 0.8 }}
              whileInView={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.5, delay: 0.3 }}
            >
              <div className="text-4xl font-bold text-purple-600">24/7</div>
              <div className="text-gray-600">Поддръжка</div>
            </motion.div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-20 bg-gray-50">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6 }}
            className="text-center mb-16"
          >
            <h2 className="text-4xl font-bold text-gray-900 mb-4">
              Защо да изберете нас?
            </h2>
            <p className="text-xl text-gray-600 max-w-2xl mx-auto">
              Обединяваме най-добрите практики и иновативни методи за вашето обучение
            </p>
          </motion.div>

          <div className="grid md:grid-cols-3 gap-8">
            {features.map((feature, index) => (
              <motion.div
                key={index}
                variants={fadeInUp}
                initial="initial"
                whileInView="animate"
                viewport={{ once: true }}
                className="card p-8 text-center"
              >
                <div className="flex justify-center mb-4">
                  {feature.icon}
                </div>
                <h3 className="text-xl font-semibold mb-3">{feature.title}</h3>
                <p className="text-gray-600">{feature.description}</p>
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      {/* Courses Section */}
      <section className="py-20 bg-white">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6 }}
            className="text-center mb-16"
          >
            <h2 className="text-4xl font-bold text-gray-900 mb-4">
              Популярни курсове
            </h2>
            <p className="text-xl text-gray-600 max-w-2xl mx-auto">
              Изберете от нашата колекция от професионални курсове
            </p>
          </motion.div>

          <div className="grid md:grid-cols-3 gap-8">
            {courses.map((course, index) => (
              <motion.div
                key={course.id}
                variants={fadeInUp}
                initial="initial"
                whileInView="animate"
                viewport={{ once: true }}
                transition={{ delay: index * 0.1 }}
              >
                <CourseCard course={course} />
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      <Footer />
    </div>
  )
}
