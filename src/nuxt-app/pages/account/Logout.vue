<script setup lang="ts">
definePageMeta({
  // This is an example of inline middleware
  middleware: (to) => {
    const authStore = useAuthStore()
    authStore.logout()

    const returnUrl = to.query.returnUrl as string
    if (returnUrl) return navigateTo(returnUrl)
    return navigateTo('/')
  },
})
</script>
