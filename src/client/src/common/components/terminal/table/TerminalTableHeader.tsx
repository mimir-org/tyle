import { Th, Tr } from "complib/data-display";
import { Text } from "complib/text";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useMediaQuery } from "../../../../hooks/useMediaQuery";

export const TerminalTableHeader = () => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const textColor = theme.tyle.color.sys.primary.base;

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
