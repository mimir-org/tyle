import { useTheme } from "styled-components";
import textResources from "../../../../../../assets/text/TextResources";
import { Heading } from "../../../../../../complib/text";
import { TerminalTable } from "../../../../../common/terminal";
import { NodeItem } from "../../../../../types/NodeItem";

export const NodePanelTerminals = ({ terminals }: Pick<NodeItem, "terminals">) => {
  const theme = useTheme();

  return (
    <>
      <Heading as={"h3"} variant={"body-large"} color={theme.tyle.color.sys.surface.on}>
        {textResources.TERMINAL_TITLE}
      </Heading>
      <TerminalTable terminals={terminals} />
    </>
  );
};
