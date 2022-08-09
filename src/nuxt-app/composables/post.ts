import { $fetch } from 'ohmyfetch'

export const $post = $fetch.create({
  baseURL: '/api',
  method: 'POST',
  async onRequest({ options }) {
    const authStore = useAuthStore()
    if (!authStore.isAuthenticated) return
    // await authStore.refresh()
    options.headers = new Headers(options.headers)
    options.headers.set(
      'Authorization',
      `Bearer ${authStore.authInfo?.accessToken}`
    )
  },
})
