import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Heading } from "../../../../../../complib/text";
import { TerminalTable } from "../../../../../common/terminal";
import { NodeItem } from "../../../../../types/NodeItem";

export const NodePanelTerminals = ({ terminals }: Pick<NodeItem, "terminals">) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });

  return (
    <>
      <Heading as={"h3"} variant={"body-large"} color={theme.tyle.color.sys.surface.on}>
        {t("title")}
      </Heading>
      <TerminalTable terminals={terminals} />
    </>
  );
};
