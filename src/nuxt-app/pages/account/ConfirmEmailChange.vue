<script setup lang="ts">
const authStore = useAuthStore()

let message = $ref('')
const route = useRoute()

onMounted(async () => {
  const userId = route.query.userId
  const code = route.query.code
  const email = route.query.email

  if (!userId || !code || !email) navigateTo('/')

  let url = '/account/confirmemailchange'

  if (authStore.isAuthenticated()) url = '/account/manage/confirmemailchange'

  try {
    const response = await $postBody(url, { userId, code, email })
    if (authStore.isAuthenticated() && response.token)
      authStore.login(response.token)
    message = response.data.message
  } catch {
    navigateTo('/')
  }
})
</script>

<template>
  <h1>Confirm Email Change</h1>
  <TwAlertSuccess>{{ message }}</TwAlertSuccess>
</template>
