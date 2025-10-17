'use client'

import { useState } from 'react'
import { motion } from 'framer-motion'
import { Menu, X, User, ShoppingCart } from 'lucide-react'

export default function Header() {
  const [isMenuOpen, setIsMenuOpen] = useState(false)

  const menuItems = [
    { name: 'Курсове', href: '/courses' },
    { name: 'За нас', href: '/about' },
    { name: 'Контакти', href: '/contact' },
    { name: 'Блог', href: '/blog' },
  ]

  return (
    <header className="bg-white shadow-lg sticky top-0 z-50">
      <div className="container mx-auto px-4">
        <div className="flex justify-between items-center h-16">
          {/* Logo */}
          <motion.div
            initial={{ opacity: 0, x: -20 }}
            animate={{ opacity: 1, x: 0 }}
            className="flex items-center"
          >
            <span className="text-2xl font-bold text-primary-600">
              Стани<span className="text-gold-500">Богат</span>
            </span>
          </motion.div>

          {/* Desktop Navigation */}
          <nav className="hidden md:flex items-center space-x-8">
            {menuItems.map((item, index) => (
              <motion.a
                key={item.name}
                href={item.href}
                initial={{ opacity: 0, y: -20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: index * 0.1 }}
                className="text-gray-700 hover:text-primary-600 font-medium transition-colors"
              >
                {item.name}
              </motion.a>
            ))}
          </nav>

          {/* Desktop Actions */}
          <div className="hidden md:flex items-center space-x-4">
            <motion.button
              initial={{ opacity: 0, scale: 0.8 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ delay: 0.5 }}
              className="p-2 text-gray-700 hover:text-primary-600 transition-colors"
            >
              <ShoppingCart className="w-5 h-5" />
            </motion.button>
            
            <motion.button
              initial={{ opacity: 0, scale: 0.8 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ delay: 0.6 }}
              className="p-2 text-gray-700 hover:text-primary-600 transition-colors"
            >
              <User className="w-5 h-5" />
            </motion.button>
            
            <motion.button
              initial={{ opacity: 0, scale: 0.8 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ delay: 0.7 }}
              className="btn-primary"
            >
              Влез
            </motion.button>
          </div>

          {/* Mobile Menu Button */}
          <button
            className="md:hidden p-2"
            onClick={() => setIsMenuOpen(!isMenuOpen)}
          >
            {isMenuOpen ? (
              <X className="w-6 h-6" />
            ) : (
              <Menu className="w-6 h-6" />
            )}
          </button>
        </div>

        {/* Mobile Navigation */}
        {isMenuOpen && (
          <motion.div
            initial={{ opacity: 0, height: 0 }}
            animate={{ opacity: 1, height: 'auto' }}
            exit={{ opacity: 0, height: 0 }}
            className="md:hidden border-t border-gray-200"
          >
            <nav className="py-4 space-y-4">
              {menuItems.map((item) => (
                <a
                  key={item.name}
                  href={item.href}
                  className="block text-gray-700 hover:text-primary-600 font-medium transition-colors"
                  onClick={() => setIsMenuOpen(false)}
                >
                  {item.name}
                </a>
              ))}
              
              <div className="flex items-center space-x-4 pt-4 border-t border-gray-200">
                <button className="flex items-center text-gray-700 hover:text-primary-600 transition-colors">
                  <ShoppingCart className="w-5 h-5 mr-2" />
                  Количка
                </button>
                
                <button className="flex items-center text-gray-700 hover:text-primary-600 transition-colors">
                  <User className="w-5 h-5 mr-2" />
                  Профил
                </button>
                
                <button className="btn-primary w-full">
                  Влез
                </button>
              </div>
            </nav>
          </motion.div>
        )}
      </div>
    </header>
  )
}
