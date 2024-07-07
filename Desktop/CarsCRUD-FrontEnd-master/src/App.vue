<!-- App.vue -->
<template>
    <Toast />
    <header>
        <Menubar :model="items">
            <template #start>

                <span class="p-menubar-button"></span>
            </template>
        </Menubar>
    </header>
    <RouterView />
    <Toast />
</template>

<script setup>
    import { ref } from 'vue';
    import Menubar from 'primevue/menubar';
    import { useRouter } from 'vue-router';
    

    const items = ref([
        {
            label: 'Home ',
            command: () => { router.push('/') }
        },
        {
            label: ' Cars ',
            command: () => { router.push('/Cars') }
        },
        {
            label: ' Edit Cars ',
            command: () => { router.push('/EditCars') }
        },
        {
            label: ' Add Cars ',
            command: () => { router.push('/AddCars') }
        }
    ]);
    

    const router = useRouter();
</script>

<style scoped>
    .p-menubar-button {
        font-weight: bold;
        font-size: 6.25rem;
        margin-right: 1rem;
    }
</style>
