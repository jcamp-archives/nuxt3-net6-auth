<script setup lang="ts">
const authStore = useAuthStore()

let success = $ref('')
const route = useRoute()

onMounted(async () => {
  const userId = route.query.userId
  const code = route.query.code
  const email = route.query.email

  if (!userId || !code || !email) navigateTo('/')

  let url = '/account/confirmemailchange'

  if (authStore.isAuthenticated) url = '/account/manage/confirmemailchange'

  try {
    const response = await $post(url, { body: { userId, code, email } })
    if (authStore.isAuthenticated && response.token)
      authStore.login(response.token)
    success = response.data.message
  } catch {
    navigateTo('/')
  }
})
</script>

<template>
  <h1>Confirm Email Change</h1>
  <TwAlertSuccess v-if="success">
    {{ success }}
  </TwAlertSuccess>
</template>
