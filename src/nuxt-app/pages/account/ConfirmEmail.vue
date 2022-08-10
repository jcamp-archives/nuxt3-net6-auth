<script setup lang="ts">
const route = useRoute()

let success = $ref('')
let errorMessage = $ref('')

onMounted(async () => {
  const userId = route.query.userId
  const code = route.query.code

  if (!userId || !code) {
    navigateTo('/')
  }

  try {
    const response = await $post('/account/confirmemail', {
      body: { userId, code },
    })
    success = response.successMessage
  } catch (error: any) {
    errorMessage = error.data.errorMessage
  }
})
</script>

<template>
  <div>
    <h1 class="text-2xl">Confirm Email</h1>
    <TwAlertSuccess v-if="success">{{ success }}</TwAlertSuccess>
    <TwAlertDanger v-if="errorMessage">{{ errorMessage }}</TwAlertDanger>
  </div>
</template>
