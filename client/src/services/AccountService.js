import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
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

  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
