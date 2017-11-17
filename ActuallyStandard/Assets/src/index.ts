import Vue from "vue"
import VueRouter from "vue-router"
import Index from "./components/changelog/Index.vue"
import * as models from './models'

Vue.use(VueRouter)

const router = new VueRouter({
    routes: [
        { path: "/changelog", component: Index },
        { path: "/", component: Index }
    ],
    
})

const app = new Vue({
    router,
    template: "<router-view></router-view>"
    //render: (h) => h(Index),
    //template: "<Index/>",
}).$mount("#app")
