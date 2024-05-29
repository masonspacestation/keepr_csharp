/* eslint-disable no-console */
import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getMyVaults() {
    const response = await api.get(`account/vaults`)
    console.log('My vaults', response.data);
    const myVaults = response.data.map(vaultData => new Vault(vaultData))
    AppState.myVaults = myVaults
  }

  async getMyKeeps() {
    const response = await api.get(`account/keeps`)
    console.log('mykeeps', response.data);
    const myKeeps = response.data.map(keepData => new Keep(keepData))
    AppState.myKeeps = myKeeps
  }

  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async updateAccount(accountData) {
    const response = await api.put('/account', accountData)
    console.log('Updated account', response.data);
    AppState.account = new Account(response.data)
    AppState.activeProfile = new Profile(response.data)
  }
}

export const accountService = new AccountService()
