import { Card } from "@mimirorg/component-library";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import React from "react";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";

interface SymbolCardProps {
  symbol: EngineeringSymbol;
  selected?: boolean;
  onClick: () => void;
}

const SymbolCard = ({ symbol, selected = false, onClick }: SymbolCardProps) => {
  const [hover, setHover] = React.useState(false);

  return (
    <Card
      variant={selected ? "selected" : "filled"}
      onClick={onClick}
      style={{
        cursor: "pointer",
        border: selected ? `1px solid black` : undefined,
      }}
      onMouseOver={() => setHover(true)}
      onMouseOut={() => setHover(false)}
    >
      <EngineeringSymbolSvg symbol={symbol} width={200} height={200} showConnectionPoints={hover || selected} />
    </Card>
  );
};

export default SymbolCard;
