import { Fragment } from "react";
import { Dd, Dl, Dt } from "./RoleCardDetails.styled";

interface PermissionCardDetailsProps {
  descriptors: { [key: string]: string };
}

const RoleCardDetails = ({ descriptors }: PermissionCardDetailsProps) => (
  <Dl>
    {descriptors &&
      Object.keys(descriptors).map((k, i) => (
        <Fragment key={i + k}>
          <Dt>{k}</Dt>
          <Dd>{descriptors[k]}</Dd>
        </Fragment>
      ))}
  </Dl>
);

export default RoleCardDetails;
