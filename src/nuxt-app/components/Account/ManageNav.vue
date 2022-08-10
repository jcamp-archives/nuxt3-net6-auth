<script setup lang="ts">
const items = reactive([
  { title: 'Profile', link: '/account/manage', exact: true },
  { title: 'Email', link: '/Account/manage/Email', exact: true },
  { title: 'Password', link: '/Account/manage/ChangePassword', exact: true },
  {
    title: 'Two-factor authentication',
    link: '/Account/Manage/TwoFactor',
    exact: false,
  },
  {
    title: 'Personal data',
    link: '/account/manage/personaldata',
    exact: false,
  },
])
</script>

<template>
  <ul class="flex flex-col space-y-2 p-4">
    <li
      v-for="(item, i) in items"
      :key="i"
      class="nav-item"
    >
      <nuxt-link :to="item.link">
        {{ item.title }}
      </nuxt-link>
    </li>
  </ul>
</template>

<style scoped>
li a {
  @apply flex h-12 flex-row items-center rounded px-4 py-2 text-sm text-blue-600 hover:bg-gray-100;
}
li :deep(a.router-link-exact-active) {
  @apply bg-blue-600 text-white;
}
</style>
