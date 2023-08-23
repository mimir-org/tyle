import { Text, Th, Tr } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useMediaQuery } from "usehooks-ts";

export const TerminalTableHeader = () => {
  const theme = useTheme();
  const { t } = useTranslation("explore", { keyPrefix: "about.terminals" });
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const textColor = theme.mimirorg.color.primary.base;

  return (
    <Tr>
      <Th>
        <Text as={"span"} color={textColor}>
          {t("templates.terminal", { object: t("name").toLowerCase() })}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} color={textColor}>
          {t("templates.terminal", { object: t("direction").toLowerCase() })}
        </Text>
      </Th>
      <Th textAlign={adjustAmountAlignment ? "center" : "left"}>
        <Text as={"span"} color={textColor}>
          {t("amount")}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} color={textColor}>
          {t("templates.terminal", { object: t("attributes").toLowerCase() })}
        </Text>
      </Th>
    </Tr>
  );
};
