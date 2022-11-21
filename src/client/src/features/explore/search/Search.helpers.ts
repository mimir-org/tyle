import { Link } from "common/types/link";
import { interfaceFormBasePath } from "features/entities/interface/InterfaceFormRoutes";
import { nodeFormBasePath } from "features/entities/node/NodeFormRoutes";
import { terminalFormBasePath } from "features/entities/terminal/TerminalFormRoutes";
import { transportFormBasePath } from "features/entities/transport/TransportFormRoutes";

export const getCreateMenuLinks = (): Link[] => {
  return [
    {
      name: "Aspect object",
      path: nodeFormBasePath,
    },
    {
      name: "Interface",
      path: interfaceFormBasePath,
    },
    {
      name: "Terminal",
      path: terminalFormBasePath,
    },
    {
      name: "Transport",
      path: transportFormBasePath,
    },
  ];
};
