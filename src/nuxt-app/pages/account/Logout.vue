<script setup lang="ts">
const authStore = useAuthStore()

const route = useRoute()

authStore.logout()

const returnUrl = route.query.returnUrl as string
if (returnUrl) navigateTo(returnUrl)
else navigateTo('/')
</script>
