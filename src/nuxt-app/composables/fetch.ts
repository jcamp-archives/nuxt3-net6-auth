// this file is just some ideas to try to remove repetitive boilerplate code
// these are direct fetch calls, vs the useClientFetch which is nuxt3's asyncdata wrapper

import { $fetch } from 'ohmyfetch'
import type { FetchOptions, FetchRequest } from 'ohmyfetch'

const baseURL = '/api'

export async function setHeaders(options: FetchOptions) {
  const authStore = useAuthStore()
  if (!authStore.isAuthenticated()) return
  // await authStore.refresh()
  options.headers = new Headers(options.headers)
  options.headers.set(
    'Authorization',
    `Bearer ${authStore.authInfo?.accessToken}`
  )
}

export const $get = $fetch.create({
  baseURL,
  method: 'GET',
  async onRequest({ options }) {
    setHeaders(options)
  },
})

export const $post = $fetch.create({
  baseURL,
  method: 'POST',
  async onRequest({ options }) {
    setHeaders(options)
  },
})

export function $postBody<T = any>(request: FetchRequest, body?: any) {
  return $post<T>(request, { body })
}
