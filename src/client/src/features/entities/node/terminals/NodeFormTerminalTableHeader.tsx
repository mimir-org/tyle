import { useMediaQuery } from "common/hooks/useMediaQuery";
import { Th, Tr } from "complib/data-display";
import { Text } from "complib/text";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const NodeFormTerminalTableHeader = () => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });
  const adjustAttributesPadding = useMediaQuery("screen and (min-width: 1500px)");

  return (
    <Tr>
      <Th>
        <Text as={"span"} variant={"title-medium"}>
          {t("templates.terminal", { object: t("name").toLowerCase() })}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} variant={"title-medium"}>
          {t("amount")}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} variant={"title-medium"}>
          {t("templates.terminal", { object: t("direction").toLowerCase() })}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} variant={"title-medium"} pl={adjustAttributesPadding ? theme.tyle.spacing.xxxl : undefined}>
          {t("templates.terminal", { object: t("attributes").toLowerCase() })}
        </Text>
      </Th>
    </Tr>
  );
};
