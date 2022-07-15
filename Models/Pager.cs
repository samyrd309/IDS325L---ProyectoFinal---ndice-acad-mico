namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage{ get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages{ get; private set; }
        public int StartPage{ get; private set; }
        public int EndPage { get; private set; }

        public Pager()
        {

        }
        public Pager(int totalItems,  int page,int pageSize = 10)        {
            int TotalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            int currentPage = page;

            int StartPage = currentPage - 5;
            int EndPage = currentPage + 4;

            if (StartPage <= 0)
            {
                EndPage = EndPage - (StartPage - 1);
                StartPage = 1;
            }

            if (EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if (EndPage > 10)
                {
                    StartPage = EndPage - 9;
                } 
            }


        }

        


    }
}
