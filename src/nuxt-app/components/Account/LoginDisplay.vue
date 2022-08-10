<script setup lang="ts">
const route = useRoute()

const beginLogin = () => {
  navigateTo({ path: '/account/login', query: { returnUrl: route.fullPath } })
}
const beginLogout = () => {
  navigateTo({ path: '/account/logout', query: { returnUrl: route.fullPath } })
}
</script>

<template>
  <div>
    <AccountAuthorized v-slot="{ userName }">
      <div>
        <router-link to="/account/manage/">Hello, {{ userName }}!</router-link>
        <button
          class="px-4 focus:outline-none"
          @click.prevent="beginLogout"
        >
          Log out
        </button>
      </div>
    </AccountAuthorized>
    <AccountNotAuthorized>
      <div>
        <router-link to="/account/register">Register</router-link>
        <button
          class="px-4 focus:outline-none"
          @click.prevent="beginLogin"
        >
          Log in
        </button>
      </div>
    </AccountNotAuthorized>
  </div>
</template>
