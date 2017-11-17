<template>
    <div class="container">
        <h1 class="header">Changelog</h1>
        <div v-for="release in releases" :key="release.releaseVersion">
            <div class="release">
                <div class="date">{{release.releaseDate}}</div>
                <h2>{{ release.releaseVersion}}</h2>

                <p class="authors">
                    <span class="author" v-for="author in release.authors" :key="author"> {{ author }}</span>
                </p>

                <ol>
                    <li v-for="item in release.workItems" :key="item.id">
                        <strong>{{ item.workItemString }}</strong>
                        {{ item.description }}
                    </li>
                </ol>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import VueRouter from 'vue-router';
import { Release } from "../../models";

Vue.use(VueRouter)

interface Data {
    releases: Release[]
}

export default Vue.extend({
    data (): Data {
        return {
            releases: []
        }
    },
    beforeRouteEnter (_1, _2, next ) {
        console.log(_1, _2, next);
        console.log(this);
        (async () => {
            const response = await fetch("/api/changelog")
            const releases = await response.json()
            next((vm: any) => vm.setReleases(releases))
        })()
    },
    methods: {
        setReleases (releases: Release[]): void {
            this.releases = releases
        }
    }
})
</script>

<style lang="scss">
$blue: #0000ff;

.header {
    color: $blue
}
</style>