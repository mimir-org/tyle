import { Fragment } from "react";
import { Dd, Dl, Dt } from "./RoleCardDetails.styled";

interface RoleCardDetailsProps {
  descriptors: { [key: string]: string };
}

const RoleCardDetails = ({ descriptors }: RoleCardDetailsProps) => (
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
