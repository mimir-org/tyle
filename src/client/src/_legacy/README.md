# Previous Type Library Client

## Purpose
This directory holds most of the logic that were used in the previous edition of the type library client.  
This is kept here as an easy access reference when implementing the new client which will ultimately be very  
different from the original client. Therefore, some parts of the old client will be hard to reuse.  
Still, the contents serve as an inspiration for the new client, and reuse is encouraged where possible/appropriate.

## Rules

No part of the application should have a dependency upon _legacy and/or its contents.  
If some logic is to be reused it should be moved out of this directory and adapted to the rest of the client.