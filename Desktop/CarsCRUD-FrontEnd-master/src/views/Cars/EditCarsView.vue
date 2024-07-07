<template>
    <main>
        <h1 style="text-align:center">Editeaza masina</h1>
        <form @submit.prevent="handleSubmit" class="car-form">
            <div class="form-group">
                <label for="marca">Marca:</label>
                <input type="text" id="marca" v-model="marca" :class="{ 'is-invalid': marcaError }" />
                <span v-if="marcaError" class="error-message">Campul este obligatoriu.</span>
            </div>
            <div class="form-group">
                <label for="model">Model:</label>
                <input type="text" id="model" v-model="model" :class="{ 'is-invalid': modelError }" />
                <span v-if="modelError" class="error-message">Campul este obligatoriu.</span>
            </div>
            <div class="form-group">
                <label for="an">An:</label>
                <input type="number" id="an" v-model="an" :class="{ 'is-invalid': anError }" />
                <span v-if="anError" class="error-message">Campul este obligatoriu.</span>
            </div>
            <div class="form-group">
                <label for="motor">Motor:</label>
                <input type="text" id="motor" v-model="motor" :class="{ 'is-invalid': motorError }" />
                <span v-if="motorError" class="error-message">Campul este obligatoriu.</span>
            </div>
            <Button label="Submit" type="submit" class="submit-button" />
        </form>
    </main>
</template>

<script>
import Button from 'primevue/button';
import { ref } from 'vue';

export default {
    name: 'EditCars',
    components: {
        Button,
    },
    setup() {
        const marca = ref('');
        const model = ref('');
        const an = ref('');
        const motor = ref('');

        const marcaError = ref(false);
        const modelError = ref(false);
        const anError = ref(false);
        const motorError = ref(false);

        const validateForm = () => {
            marcaError.value = !marca.value;
            modelError.value = !model.value;
            anError.value = !an.value;
            motorError.value = !motor.value;

            return !marcaError.value && !modelError.value && !anError.value && !motorError.value;
        };

        const handleSubmit = () => {
            if (validateForm()) {
                goToCars();
            }
        };

        const goToCars = () => {
            this.$router.push({
                name: 'Cars',
                state: {
                    showMessage: true,
                    toastMessage: {
                        severity: 'success',
                        summary: 'Succes!',
                        detail: 'Masina a fost editata.'
                    }
                }
            });
        };

        return {
            marca, model, an, motor,
            marcaError, modelError, anError, motorError,
            handleSubmit
        };
    }
};
</script>
