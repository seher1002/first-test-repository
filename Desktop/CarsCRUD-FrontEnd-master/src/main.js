import './assets/main.css'
import { createApp } from 'vue'
import PrimeVue from 'primevue/config'
import App from './App.vue'
import Aura from '@primevue/themes/aura'
import router from './router'
import Menubar from 'primevue/menubar';
import ToggleSwitch from 'primevue/toggleswitch';
import 'primeicons/primeicons.css';         
import Button from 'primevue/button';
import Checkbox from 'primevue/checkbox';
import Knob from 'primevue/knob';
import 'primeicons/primeicons.css';
import Toast from 'primevue/toast';
import ToastService from 'primevue/toastservice';
import Message from 'primevue/message';
import ConfirmPopup from 'primevue/confirmpopup';
import ConfirmationService from 'primevue/confirmationservice';
import 'primeflex/primeflex.css'
import Dropdown from 'primevue/dropdown';
import Select from 'primevue/select';



const app = createApp(App);


app.use(PrimeVue, {
    // Default theme configuration
    theme: {
        preset: Aura,
        options: {
            prefix: 'p',
            darkModeSelector: '.my-app-dark',
            cssLayer: true
        }
    }
});
app.use(router);
app.use(ToastService);
app.use(ConfirmationService);

app.component('Select', Select);
app.component('ConfirmPopup', ConfirmPopup);
app.component('Dropdown', Dropdown);
app.component('Message', Message);
app.component('Toast', Toast);
app.component('Knob', Knob);
app.component('Checkbox', Checkbox);
app.component('Menubar', Menubar);
app.component('ToggleSwitch', ToggleSwitch);
app.component('Button', Button);


app.mount('#app');
