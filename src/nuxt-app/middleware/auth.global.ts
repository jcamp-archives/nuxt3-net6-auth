export default defineNuxtRouteMiddleware((to) => {
  const authStore = useAuthStore()

  if (to.meta.auth) {
    if (!authStore.isAuthenticated()) {
      // this route requires auth, check if logged in
      // if not, redirect to login page.
      return navigateTo({
        path: '/account/login',
        // save the location we were at to come back later
        query: { returnUrl: to.fullPath },
      })
    }
    const role = to.meta.role as string
    if (role && !authStore.isUserInRole(role)) {
      return navigateTo('/account/nopermission')
    }
    const roles = to.meta.roles as string[]
    if (roles && !authStore.isUserInAnyRole(roles)) {
      return navigateTo('/account/nopermission')
    }
  }
})
