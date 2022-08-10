<script setup>
definePageMeta({
  auth: true,
})
</script>

<template>
  <div>
    <h1 class="text-2xl">Manage your account</h1>

    <TwCard title="Change your account settings">
      <div class="flex flex-row">
        <div>
          <AccountManageNav></AccountManageNav>
        </div>
        <div class="w-full pl-5 pr-10">
          <Nuxt-Page />
        </div>
      </div>
    </TwCard>
  </div>
</template>
