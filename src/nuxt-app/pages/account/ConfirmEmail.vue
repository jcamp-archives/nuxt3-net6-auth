<script setup lang="ts">
const route = useRoute()

let message = $ref('')
let errorMessage = $ref('')

onMounted(async () => {
  const userId = route.query.userId
  const code = route.query.code

  if (!userId || !code) {
    navigateTo('/')
  }

  try {
    const response = await $postBody('/account/confirmemail', { userId, code })
    message = response.successMessage
  } catch (error: any) {
    errorMessage = error.data.errorMessage
  }
})
</script>

<template>
  <div>
    <h1 class="text-2xl">Confirm Email</h1>
    <TwAlertSuccess>{{ message }}</TwAlertSuccess>
    <TwAlertDanger>{{ errorMessage }}</TwAlertDanger>
  </div>
</template>
