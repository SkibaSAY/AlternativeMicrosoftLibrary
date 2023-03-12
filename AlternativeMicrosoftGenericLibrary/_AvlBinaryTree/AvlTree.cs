using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternativeMicrosoftGenericLibrary
{
    public class TreeNode<TKey, TValue> where TKey : IComparable
    {
        public TreeNode<TKey, TValue> parrent;
        public TreeNode<TKey, TValue> leftChild;
        public TreeNode<TKey, TValue> rightChild;

        public TKey key;
        public TValue value;

        #region Оптимизации, отличные от лекции
        private int height;

        /// <summary>
        /// По сути это обёртка для случая, когда node == null
        /// Используется при подсчёте фавтора баланса узла
        /// </summary>
        /// <param name="node"></param>
        /// <returns>По переданному узлу определяет, его высоту</returns>
        public int Height(TreeNode<TKey, TValue> node)
        {
            return node == null ? 0 : node.height;
        }

        /// <summary>
        /// Пересчитывает высоту узла, если уже посчитаны высоты дочерних узлов
        /// Такое достигается, когда мы проверяем баланс фактор у всех узлов, которые выше нашего, при балансировке
        /// </summary>
        public void RefreshHeight()
        {
            var leftHeight = Height(leftChild);
            var rightHeight = Height(rightChild);
            height = (leftHeight < rightHeight ? rightHeight : leftHeight) + 1;
        }

        /// <summary>
        /// Принимает значения: -1,0,1
        /// Значения описывают баланс узла:
        /// -1 - левое поддерево больше
        /// 0 - левое и правое поддеревья равны
        /// 1 - баланс нарушен в пользу правого дерева
        /// > 2 требуется балансировка из-за правого дерева
        /// < -2 требуется балансировка из-за левого дерева
        /// </summary>
        public int balanceFactor
        {
            get
            {
                return Height(leftChild) - Height(rightChild);
            }
        }
        #endregion

        public TreeNode(TKey key,TValue value)
        {
            this.key = key;
            this.value = value;
            height = 1;
        }

        public override string ToString()
        {
            return key.ToString();
        }
    }
    public class AvlTree<TKey,TValue> where TKey: IComparable
    {
        public TreeNode<TKey, TValue> _root;

        private int _count;
        public int Count
        {
            get { return _count; }
            private set
            {
                _count = value;
            }
        }

        public AvlTree()
        {

        }
        public TValue this[TKey key]
        {
            get
            {
                return FindNode(key).value;
            }
            set
            {
                FindNode(key).value = value;
            }
        }

        #region Methods
        public void Add(TKey newKey,TValue newValue)
        {
            var newNode = new TreeNode<TKey, TValue>(newKey, newValue);

            if(_root == null)
            {
                _root = newNode;
                _count++;
                return;
            }

            TreeNode<TKey, TValue> parrent = null;
            TreeNode<TKey, TValue> current = _root;

            var compareResult = 0;
            while(current!= null)
            {
                parrent = current;
                compareResult = newKey.CompareTo(current.key);
                if(compareResult == -1)//left tree
                {
                    current = current.leftChild;
                }
                else if(compareResult == 1)//right tree
                {
                    current = current.rightChild;
                }
                else//argument Exception
                {
                    throw new ArgumentException("Such key is already added");
                }
            }

            if (compareResult == -1)//left tree
            {
                parrent.leftChild = newNode;
            }
            else//right tree
            {
                parrent.rightChild = newNode;
            }

            newNode.parrent = parrent;

            _count++;
            BalanceTree(newNode.parrent);
        }

        public void Remove(TKey searchedKey)
        {
            var searchedNode = FindNode(searchedKey);
            if (searchedNode == null)
            {
                throw new ArgumentException("Such key is already remove");
            }
            RemoveNode(searchedNode);
        }
        private void RemoveNode(TreeNode<TKey,TValue> node)
        {
            var leftIsNull = node.leftChild == null;
            var rightIsNull = node.rightChild == null;

            var nodeForStartBalance = node.parrent;

            if (leftIsNull && rightIsNull)//нет потомков - удаляем ссылку у родителя
            {
                if(node.parrent == null)// если node - вершина дерева
                {
                    _root = null;
                }
                else if(node.parrent.leftChild != null && node.parrent.leftChild.key.CompareTo(node.key) == 0)
                {
                    node.parrent.leftChild = null;
                }
                else
                {
                    node.parrent.rightChild = null;
                }
            }
            //TODO: дописать после написания балансировки
            else if(!leftIsNull && !rightIsNull)//есть оба потомка - заменяем самым левым или самым правым
            {
                var current = node;

                //if(balance == -1)//ищем подмену(=самый левый) в правой ветке
                //{
                //    current = node.rightChild;
                //    while (current.leftChild != null)
                //    {
                //        current = current.leftChild;
                //    }

                //    current.parrent.leftChild = current.rightChild;
                //    if (current.rightChild != null)
                //    {
                //        current.rightChild.parrent = current.parrent;
                //    }
                //}

                //ищем подмену(=самый правый) в левой ветке
                current = node.leftChild;
                while (current.rightChild != null)
                {
                    current = current.rightChild;
                }

                //устанавливаем узел, с которого будем балансировать
                //1.удаляем 2 и сразу попадаем на заместителя
                //          4
                //      2      5           
                //    1   3          6
                nodeForStartBalance = current.parrent.key.CompareTo(node.key) != 0 ? current.parrent : current;

                //удалится одним из простых способов
                RemoveNode(current);

                //потому что в RemoveNode мы уменьшили _count, но мы же сохранили удаляемого заместителя
                //и заместителя прикручиваем на место удаляемого узла => _count нужно на 1 увеличить
                _count++;

                current.leftChild = node.leftChild;
                if(current.leftChild!= null)
                {
                    current.leftChild.parrent = current;
                }

                current.rightChild = node.rightChild;
                if(current.rightChild!= null)
                {
                    current.rightChild.parrent = current;
                }               

                var nodeParrent = node.parrent;
                var currentParrent = current.parrent;

                //заменяем ссылку на родителя
                current.parrent = node.parrent;

                #region заменяем ссылку удаляемого потомка на заместителя у родителя

                if (nodeParrent == null)//значит node = _root
                {
                    _root = current;
                }
                else if (nodeParrent.leftChild != null && nodeParrent.leftChild.key.CompareTo(node.key) == 0)//левый потомок родителя = node
                {
                    nodeParrent.leftChild = current;
                }
                else//правый потомок родителя = node
                {
                    nodeParrent.rightChild = current;
                }
                #endregion
            }
            else//если у удаляемого есть только 1 потомок
            {
                var notNullChild = node.leftChild == null ? node.rightChild : node.leftChild;

                notNullChild.parrent = node.parrent;

                if (node.parrent == null)
                {
                    _root = notNullChild;
                }
                else if (node.parrent.leftChild != null && node.parrent.leftChild.key.CompareTo(node.key) == 0)//если node - левый потомок родителя
                {
                    node.parrent.leftChild = notNullChild;
                }
                else
                {
                    node.parrent.rightChild = notNullChild;
                }
            }

            _count--;

            BalanceTree(nodeForStartBalance);
        }

        public bool Contains(TKey searchedKey)
        {
            return FindNode(searchedKey) != null;
        }
        private TreeNode<TKey, TValue> FindNode(TKey searchedKey)
        {
            if (_root == null) return null;

            TreeNode<TKey, TValue> current = _root;
            while (current != null)
            {
                if(current.key.CompareTo(searchedKey) == -1)
                {
                    current = current.rightChild;
                }
                else if (current.key.CompareTo(searchedKey) == 1)
                {
                    current = current.leftChild;
                }
                else
                {
                    return current;
                }
            }
            return null;
        }

        /// <summary>
        /// Балансировка дерева
        /// Использует проверку баланс фактора у всех узлов на пути от листа к корню
        /// Если фактор баланса нарушился(стал -2 или 2), то решаем проблему не более, чем одним поворотом
        /// </summary>
        private void BalanceTree(TreeNode<TKey, TValue> startNode)
        {
            var current = startNode;
            while(current!= null)
            {
                //Важный момент, из-за этого соблюдается корректное значение для высоты узла
                current.RefreshHeight();

                var balanceFactor = current.balanceFactor;

                //баланс нарушен
                if (balanceFactor == 2)//левое дерево нарушает баланс
                {
                    var lChild = current.leftChild;

                    //случай, когда правое поддерево левого потомка больше левого поддерева левого потомка
                    //=> нам нужно сделать дополнительный поворот левого потомка налево
                    if (lChild.balanceFactor < 0)
                    {
                        RotateLeft(lChild);
                    }
                    RotateRight(current);
                }
                else if(balanceFactor == -2)//правое дерево нарушает баланс
                {
                    var rChild = current.rightChild;

                    //случай, когда левое поддерево правого потомка больше правого поддерева правого потомка
                    //=> нам нужно сделать дополнительный поворот правого потомка направо
                    if (rChild.balanceFactor > 0)
                    {
                        RotateRight(rChild);
                    }
                    RotateLeft(current);
                }
                current = current.parrent;
            }
        }

        /// <summary>
        /// Поворачивает дерево налево
        /// 
        ///         S                           R
        ///       /   \                       /   \
        ///     A      R           =>        S     C
        ///           /  \                  / \
        ///          B    C                A   B
        ///          
        /// </summary>
        public void RotateLeft(TreeNode<TKey, TValue> node)
        {
            var S = node;
            var A = S.leftChild;
            var R = S.rightChild;

            S.rightChild = R.leftChild;
            if (R.leftChild != null) R.leftChild.parrent = S; //B.parrent = S

            R.parrent = S.parrent;
            R.leftChild = S;
            S.parrent = R;

            UpdateParrentAndHeightAfterRotate(R, S);
        }

        /// <summary>
        /// Поворачивает дерево направо
        /// 
        ///         S                         R
        ///       /   \                     /   \
        ///      R     C           =>      A     S
        ///     / \                             / \
        ///    A   B                           B   C
        ///          
        /// </summary>
        public void RotateRight(TreeNode<TKey, TValue> node)
        {
            var S = node;
            var R = S.leftChild;
            var C = S.rightChild;

            S.leftChild = R.rightChild;
            if (R.rightChild != null) R.rightChild.parrent = S; //B.parrent = S

            R.parrent = S.parrent;
            R.rightChild = S;
            S.parrent = R;

            UpdateParrentAndHeightAfterRotate(R, S);
        }

        /// <summary>
        /// Общий метод для поворота налево и поворота направо
        /// </summary>
        /// <param name="R">Вершина после повторота</param>
        /// <param name="S">Вершина до поворота</param>
        private void UpdateParrentAndHeightAfterRotate(TreeNode<TKey, TValue> R, TreeNode<TKey, TValue> S)
        {
            //компенсируем переставновку
            S.RefreshHeight();
            R.RefreshHeight();

            var parrent = R.parrent;
            if (parrent == null)
            {
                _root = R;
                return;
            }

            if (parrent.leftChild != null && parrent.leftChild.key.CompareTo(S.key) == 0)
            {
                parrent.leftChild = R;
            }
            else if (parrent.rightChild != null && parrent.rightChild.key.CompareTo(S.key) == 0)
            {
                parrent.rightChild = R;
            }
            else
            {
                throw new ArgumentException("Ошибка программиста: родитель узла R != null, при этом его потомки не S - проверить логику поворотов налево/направо");
            }
        }
        #endregion
    }
}
