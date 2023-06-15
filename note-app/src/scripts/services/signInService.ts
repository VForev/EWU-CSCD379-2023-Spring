import Axios from 'axios'

class NoteAppToken {
  userName: string = ''
}

export class SignInService {
  private _rawToken: string | null = null
  private _token: NoteAppToken = new NoteAppToken()
  private _isSignedIn: boolean = false
  private static _instance = new SignInService()
  private _tokenLocalStorageKey = 'token'

  private constructor() {
    this.setToken(localStorage.getItem(this._tokenLocalStorageKey))
  }

  public async signInAsync(username: string, password: string): Promise<boolean> {
    try {
      const result = await Axios.post('Token', {
        username: username,
        password: password
      })
      this.setToken(result.data.token)
      this._isSignedIn = true
      return true
    } catch (err) {
      console.log(`Login failed: ${err}`)
      this.signOut()
      return false
    }
  }

  public signOut() {
    this.setToken(null)
  }

  public get isSignedIn() {
    return this._isSignedIn
  }

  private setToken(token: string | null) {
    if (!token) {
      // Clear the token
      this._token = new NoteAppToken()
      this._isSignedIn = false
      this._rawToken = ''
      localStorage.setItem(this._tokenLocalStorageKey, '')
    } else {
      // Set the token
      this._rawToken = token
      localStorage.setItem(this._tokenLocalStorageKey, this._rawToken)
      this._isSignedIn = true
      Axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
      const parts = token.split('.')
      const payload = JSON.parse(window.atob(parts[1]))
      this._token = Object.assign(new NoteAppToken(), payload)
    }
  }

  public get token(): NoteAppToken {
    return this._token
  }

  public static get instance(): SignInService {
    return SignInService._instance
  }
}
