import { Facebook, Twitter, Instagram, Mail, Phone, MapPin } from 'lucide-react'

export default function Footer() {
  const footerLinks = {
    company: [
      { name: 'За нас', href: '/about' },
      { name: 'Кариери', href: '/careers' },
      { name: 'Партньори', href: '/partners' },
      { name: 'Новини', href: '/news' },
    ],
    courses: [
      { name: 'Всички курсове', href: '/courses' },
      { name: 'Безплатни курсове', href: '/free-courses' },
      { name: 'Премиум курсове', href: '/premium-courses' },
      { name: 'Корпоративно обучение', href: '/corporate' },
    ],
    support: [
      { name: 'Помощ', href: '/help' },
      { name: 'Контакти', href: '/contact' },
      { name: 'FAQ', href: '/faq' },
      { name: 'Техническа поддръжка', href: '/support' },
    ],
    legal: [
      { name: 'Условия за ползване', href: '/terms' },
      { name: 'Политика за поверителност', href: '/privacy' },
      { name: 'Бисквитки', href: '/cookies' },
      { name: 'Възстановяване', href: '/refund' },
    ],
  }

  const socialLinks = [
    { name: 'Facebook', icon: Facebook, href: '#' },
    { name: 'Twitter', icon: Twitter, href: '#' },
    { name: 'Instagram', icon: Instagram, href: '#' },
  ]

  return (
    <footer className="bg-gray-900 text-white">
      <div className="container mx-auto px-4 py-16">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-6 gap-8">
          {/* Brand Section */}
          <div className="lg:col-span-2">
            <div className="mb-6">
              <span className="text-3xl font-bold text-primary-400">
                Стани<span className="text-gold-400">Богат</span>
              </span>
            </div>
            
            <p className="text-gray-300 mb-6 max-w-md">
              Вашият път към финансовия успех започва тук. Научете от най-добрите експерти 
              и създайте своето богатство.
            </p>

            {/* Contact Info */}
            <div className="space-y-3">
              <div className="flex items-center text-gray-300">
                <Mail className="w-5 h-5 mr-3 text-primary-400" />
                <span>info@stanibogat.bg</span>
              </div>
              
              <div className="flex items-center text-gray-300">
                <Phone className="w-5 h-5 mr-3 text-primary-400" />
                <span>+359 888 123 456</span>
              </div>
              
              <div className="flex items-center text-gray-300">
                <MapPin className="w-5 h-5 mr-3 text-primary-400" />
                <span>София, България</span>
              </div>
            </div>

            {/* Social Links */}
            <div className="flex space-x-4 mt-6">
              {socialLinks.map((social) => {
                const Icon = social.icon
                return (
                  <a
                    key={social.name}
                    href={social.href}
                    className="p-2 bg-gray-800 hover:bg-primary-600 rounded-lg transition-colors"
                  >
                    <Icon className="w-5 h-5" />
                  </a>
                )
              })}
            </div>
          </div>

          {/* Company Links */}
          <div>
            <h3 className="text-lg font-semibold mb-4">Компания</h3>
            <ul className="space-y-2">
              {footerLinks.company.map((link) => (
                <li key={link.name}>
                  <a
                    href={link.href}
                    className="text-gray-300 hover:text-primary-400 transition-colors"
                  >
                    {link.name}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          {/* Courses Links */}
          <div>
            <h3 className="text-lg font-semibold mb-4">Курсове</h3>
            <ul className="space-y-2">
              {footerLinks.courses.map((link) => (
                <li key={link.name}>
                  <a
                    href={link.href}
                    className="text-gray-300 hover:text-primary-400 transition-colors"
                  >
                    {link.name}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          {/* Support Links */}
          <div>
            <h3 className="text-lg font-semibold mb-4">Поддръжка</h3>
            <ul className="space-y-2">
              {footerLinks.support.map((link) => (
                <li key={link.name}>
                  <a
                    href={link.href}
                    className="text-gray-300 hover:text-primary-400 transition-colors"
                  >
                    {link.name}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          {/* Legal Links */}
          <div>
            <h3 className="text-lg font-semibold mb-4">Правна информация</h3>
            <ul className="space-y-2">
              {footerLinks.legal.map((link) => (
                <li key={link.name}>
                  <a
                    href={link.href}
                    className="text-gray-300 hover:text-primary-400 transition-colors"
                  >
                    {link.name}
                  </a>
                </li>
              ))}
            </ul>
          </div>
        </div>

        {/* Newsletter Subscription */}
        <div className="border-t border-gray-800 mt-12 pt-8">
          <div className="max-w-md">
            <h3 className="text-lg font-semibold mb-4">
              Абонирайте се за нашия бюлетин
            </h3>
            <p className="text-gray-300 mb-4">
              Получавайте най-новите съвети и оферти директно в пощата си
            </p>
            
            <div className="flex">
              <input
                type="email"
                placeholder="Въведете имейл адрес"
                className="flex-1 px-4 py-2 bg-gray-800 border border-gray-700 rounded-l-lg focus:outline-none focus:border-primary-500 text-white"
              />
              <button className="px-6 py-2 bg-primary-600 hover:bg-primary-700 rounded-r-lg transition-colors">
                Абонирай се
              </button>
            </div>
          </div>
        </div>

        {/* Bottom Bar */}
        <div className="border-t border-gray-800 mt-8 pt-8">
          <div className="flex flex-col md:flex-row justify-between items-center">
            <p className="text-gray-400 text-sm">
              © 2024 СтаниБогат. Всички права запазени.
            </p>
            
            <div className="flex space-x-6 mt-4 md:mt-0">
              <a href="/terms" className="text-gray-400 hover:text-primary-400 text-sm transition-colors">
                Условия за ползване
              </a>
              <a href="/privacy" className="text-gray-400 hover:text-primary-400 text-sm transition-colors">
                Политика за поверителност
              </a>
            </div>
          </div>
        </div>
      </div>
    </footer>
  )
}
