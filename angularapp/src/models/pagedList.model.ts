export interface PagedList<T> {
    currentPage: number;
    pageCount: number;
    pageSize: number;
    rowCount: number;
    data?: T[] | null;
    firstRowOnPage: number;
    lastRowOnPage: number;
  }
  