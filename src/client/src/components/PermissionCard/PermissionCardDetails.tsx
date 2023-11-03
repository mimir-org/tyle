import { Fragment } from "react";
import { Dd, Dl, Dt } from "./PermissionCardDetails.styled";

interface PermissionCardDetailsProps {
  descriptors: { [key: string]: string };
}

const PermissionCardDetails = ({ descriptors }: PermissionCardDetailsProps) => (
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

export default PermissionCardDetails;
