import { interfaceFormBasePath } from "../../../entities/interface/InterfaceFormRoutes";
import { nodeFormBasePath } from "../../../entities/node/NodeFormRoutes";
import { terminalFormBasePath } from "../../../entities/terminal/TerminalFormRoutes";
import { transportFormBasePath } from "../../../entities/transport/TransportFormRoutes";
import { Link } from "../../types/link";

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
