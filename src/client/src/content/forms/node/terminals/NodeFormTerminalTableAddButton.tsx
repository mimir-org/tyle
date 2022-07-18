import { PlusSm } from "@styled-icons/heroicons-outline";
import textResources from "../../../../assets/text/TextResources";
import { Button } from "../../../../complib/buttons";

export const NodeFormTerminalTableAddButton = ({ onClick }: { onClick: () => void }) => (
  <Button icon={<PlusSm />} iconOnly onClick={onClick}>
    {textResources.TERMINAL_ADD}
  </Button>
);
