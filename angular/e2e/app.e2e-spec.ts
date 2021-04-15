import { LibraryManagementProjectTemplatePage } from './app.po';

describe('LibraryManagementProject App', function() {
  let page: LibraryManagementProjectTemplatePage;

  beforeEach(() => {
    page = new LibraryManagementProjectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
