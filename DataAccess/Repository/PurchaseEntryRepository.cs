﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
   public class PurchaseEntryRepository
    {
        private Stock _stock;
        public void InsertOrUpdateInventory(PurchaseItem vm)
        {
            using (ASITPOSDBEntities db = new ASITPOSDBEntities())
            {
                _stock = new Stock();

                //Initialize new stock with vm inserted values
                _stock.ItemID = vm.ItemID;
                _stock.BatchNo = vm.Batch;
                _stock.CostPrice = vm.CostPrice;
                _stock.SellingPrice = vm.SellingPrice;
                _stock.ExpiryDate = vm.Expiry;
                _stock.PurchaseID = vm.PurchaseID;

                //Get list of all the inserted item in Stock table
                List<Stock> _checkItem = (from s in db.Stocks
                                          where s.ItemID == vm.ItemID && s.BatchNo == vm.Batch
                                          select s).ToList();

                //count the number of exixting record on inserted item
                int countStock = _checkItem.Count();

                //Add new record if record is not found
                if (countStock == 0)
                {
                    //Add new item with new Initial qty
                    _stock.Qty = vm.Qty;
                    _stock.InitialQty = _stock.Qty;
                    db.Stocks.Add(_stock);
                    db.SaveChanges();
                }
                else
                {
                    //to check how many times loop executes completely 
                    int loopCount = 0;
                    //Check and Add or update
                    foreach (Stock stock in _checkItem)
                    {
                        if (stock.CostPrice == vm.CostPrice)
                        {
                            //Update qty and InitialQty 
                            stock.Qty += vm.Qty;
                            stock.InitialQty += vm.Qty;
                            db.SaveChanges();
                            break;
                        }
                        loopCount++;
                    }
                    if (loopCount == _checkItem.Count())
                    {
                        //Add new record with Qty and intial Qty
                        _stock.InitialQty += vm.Qty;
                        db.Stocks.Add(_stock);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
