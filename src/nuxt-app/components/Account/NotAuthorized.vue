<script setup lang="ts">
const authStore = useAuthStore()

const isAuthorized = computed(() => authStore.isAuthenticated())
</script>

<template>
  <template v-if="!isAuthorized">
    <slot></slot>
  </template>
</template>
