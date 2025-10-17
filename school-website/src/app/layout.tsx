import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import './globals.css'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'Стани Богат - Образователна Платформа',
  description: 'Научете как да печелите пари онлайн с нашите професионални курсове',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="bg">
      <body className={inter.className}>
        {children}
      </body>
    </html>
  )
}
