import { Link } from "common/types/link";
import { interfaceFormBasePath } from "features/entities/interface/InterfaceFormRoutes";
import { nodeFormBasePath } from "features/entities/node/NodeFormRoutes";
import { terminalFormBasePath } from "features/entities/terminal/TerminalFormRoutes";
import { transportFormBasePath } from "features/entities/transport/TransportFormRoutes";
import { useTranslation } from "react-i18next";

export const useCreateMenuLinks = (): Link[] => {
  const { t } = useTranslation("explore");

  return [
    {
      name: t("search.create.aspectObject"),
      path: nodeFormBasePath,
    },
    {
      name: t("search.create.interface"),
      path: interfaceFormBasePath,
    },
    {
      name: t("search.create.terminal"),
      path: terminalFormBasePath,
    },
    {
      name: t("search.create.transport"),
      path: transportFormBasePath,
    },
  ];
};
