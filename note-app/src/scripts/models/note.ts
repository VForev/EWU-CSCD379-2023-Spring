export class Note {
  noteId: number = 0
  title: string = ''
  content: string = ''
  // TODO: Modify so the date is set when pushed to the database and not at instantiation time
  created: Date = new Date()
  // TODO: Modify so the date is set when pushed to the database and not at instantiation time
  lastModified: Date = new Date()
  urlSuffix: string = ''
  appUserID: string = ''
  stringCreated: string = ''
  stringModified: string = ''
}
