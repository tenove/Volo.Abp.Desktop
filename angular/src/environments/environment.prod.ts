import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Desktop',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44326',
    redirectUri: baseUrl,
    clientId: 'Desktop_App',
    responseType: 'code',
    scope: 'offline_access Desktop',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44388',
      rootNamespace: 'Volo.Abp.Desktop',
    },
  },
} as Environment;
