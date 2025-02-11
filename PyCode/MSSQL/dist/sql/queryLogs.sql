select *
from dbo.ClosePeriodLogs
where Month(DateExec) = {MONTH}
  and Year(DateExec) = {YEAR}
;