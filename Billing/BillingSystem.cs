using Billing.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing
{
    public class BillingSystem : IBillingSystem
    {
        private IList<CallInfo> _callList;
        public BillingSystem(IList<CallInfo> infoList)
        {
            _callList = infoList;
        }

        public Report GetReport(PhoneNumber phoneNumber)
        {
            var calls = _callList.
                Where(x => (x.MyNumber == phoneNumber) || (x.TargetNumber == phoneNumber)).
                ToList();
            var report = new Report();

            foreach (var call in calls)
            {
                CallType callType;
                PhoneNumber number;

                if (call.MyNumber == phoneNumber)
                {
                    callType = CallType.OutgoingCall;
                    number = call.TargetNumber;
                }
                else
                {
                    callType = CallType.IncomingCall;
                    number = call.MyNumber;
                }

                var record = new ReportRecord(callType, number, call.BeginCall, new DateTime((call.EndCall - call.BeginCall).Ticks), call.Cost);
                report.AddRecord(record);
            }

            return report;
        }
    }
}
