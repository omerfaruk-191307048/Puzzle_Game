using UnityEngine;
using System.Collections.Generic;

namespace CodeX.LinkedList
{
    class LinkedList : ListBASE
    {
        public override void InsertFirst(int value)
        {
            Parca_Data tmpHead = new Parca_Data
            {
                Data = value
            };
            if (Head == null)
            {
                Head = tmpHead;
                if(!tmpHead.IsHead)
                tmpHead.IsHead = true;
            }
            else
            {
                //En kritik nokta: tmpHead'in next'i eski Head'i göstermeli
                tmpHead.Next = Head;
                //Yeni Head artýk tmpHead oldu
                Head = tmpHead;
            }
            //Baðlý listedeki eleman sayýsý bir arttý
            Size++;
        }

        public override void InsertLast(int value)
        {
            Parca_Data tmpHead = new Parca_Data
            {
                Data = value
            };
            Parca_Data last;
            Parca_Data iter;
            iter = Head;

            if (iter == null)
            {
                Head = iter;
            }
            else
            {
                while (iter.Next != null)
                {
                    iter = iter.Next;
                }
                last = iter;
                last.Next = tmpHead;
            }

            Size++;
        }

        public override void InsertPos(int position, int value)
        {
            Parca_Data tmpHead = new Parca_Data
            {
                Data = value
            };

            Parca_Data iter;
            iter = Head;

            int i;
            for (i = 1; i < position - 1; i++)
            {
                iter = iter.Next;
            }
            tmpHead.Next = iter.Next;
            iter.Next = tmpHead;

            Size++;
        }

        public override void DeleteFirst()
        {
            if (Head != null)
            {
                //Head'in next'i HeadNext'e atanýyor
                Parca_Data HeadNext = this.Head.Next;
                //HeadNext null ise zaten tek kayýt olan Head silinir.
                if (HeadNext == null)
                    Head = null;
                else
                    //HeadNext null deðilse yeni Head, HeadNext olur.
                    Head = HeadNext;
                //Listedeki eleman sayýsý bir azaltýlýyor
                Size--;
            }
        }

        public override void DeleteLast()
        {
            if (Head != null)
            {
                Parca_Data iter;
                int poz = Size;
                Parca_Data tmp;
                iter = this.Head;
                int i;
                for (i = 1; i < poz - 1; i++)
                {
                    iter = iter.Next;
                }
                //iter = null;

                iter.Next = null;

                Size--;
            }
        }

        public override void DeletePos(int position)
        {
            if (Head != null)
            {
                Parca_Data iter;
                Parca_Data tmp;
                iter = this.Head;

                while (iter.Next.Data != position)
                {
                    iter = iter.Next;
                }
                tmp = iter.Next.Next;
                iter.Next = null;
                iter.Next = tmp;


                Size--;
            }
        }


        public override Parca_Data GetElement(int position) //Pozisyon gore parcanin bilgisini verir
        {
            Parca_Data iter;
            iter = this.Head;
            int i;
            for (i = 1; i < position; i++)
            {
                iter = iter.Next;
            }

            return iter;
        }

        public override string DisplayElements() //Tum elemanlari verir
        {
            string temp = "";
            Parca_Data item = Head;
            while (item != null)
            {
                temp += item.Data + "-->";
                item = item.Next;
            }

            return temp;
        }
    }
}