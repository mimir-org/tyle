import Card from "components/Card";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import React from "react";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";

interface SymbolCardProps {
  symbol: EngineeringSymbol;
  onClick: () => void;
}

const SymbolCard = ({ symbol, onClick }: SymbolCardProps) => {
  const [hover, setHover] = React.useState(false);

  return (
    <Card
      variant={hover ? "selected" : "filled"}
      onClick={onClick}
      style={{
        cursor: "pointer",
      }}
      onMouseOver={() => setHover(true)}
      onMouseOut={() => setHover(false)}
    >
      <EngineeringSymbolSvg symbol={symbol} width={200} height={200} showConnectionPoints={hover} />
    </Card>
  );
};

export default SymbolCard;
