<script setup lang="ts">
const authStore = useAuthStore()

const userName = computed(() => authStore.userName)
const isAuthorized = computed(() => authStore.isAuthenticated())
</script>

<template>
  <template v-if="isAuthorized">
    <slot :user-name="userName"></slot>
  </template>
</template>
