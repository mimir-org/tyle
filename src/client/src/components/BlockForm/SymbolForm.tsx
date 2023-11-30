import React from "react";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { BlockFormStepProps } from "./BlockForm";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";
import ConnectTerminalsToSymbolStep from "./ConnectTerminalsToSymbolStep";
import SelectSymbolStep from "./SelectSymbolStep";

const SymbolForm = React.forwardRef<HTMLFormElement, BlockFormStepProps>(({ fields, setFields }, ref) => {
  const { symbol, terminals } = fields;
  const setSymbol = (symbol: EngineeringSymbol | null) => setFields((fields) => ({ ...fields, symbol }));
  const setTerminals = (terminals: TerminalTypeReferenceField[]) => setFields((fields) => ({ ...fields, terminals }));

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      {symbol ? (
        <ConnectTerminalsToSymbolStep
          symbol={symbol}
          setSymbol={setSymbol}
          terminals={terminals}
          setTerminals={setTerminals}
        />
      ) : (
        <SelectSymbolStep setSymbol={setSymbol} />
      )}
    </form>
  );
});

SymbolForm.displayName = "SymbolForm";

export default SymbolForm;
