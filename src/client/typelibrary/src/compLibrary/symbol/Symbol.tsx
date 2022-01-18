import SymbolImage from "./styled/SymbolImage";

interface Props {
  base64: string;
  text: string;
}

const Symbol = ({ base64, text }: Props) => {
  if (base64 === null || base64 === undefined) return null;
  return <SymbolImage src={"data:image/svg+xml;base64," + base64} alt={text} draggable="false" />;
};

export default Symbol;
