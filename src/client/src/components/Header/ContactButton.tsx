import Box from "components/Box";
import Dialog from "components/Dialog";
import Select from "components/Select";

/**
 * Component that displays a button with a dialog for finding contact information about
 * the various organizations that the user has permissions to view.
 *
 * @constructor
 */
const ContactButton = () => {
  //const [selected, setSelected] = useState<number>();

  //const companies = useGetFilteredCompanies(MimirorgPermission.Read);
  //const { data: company } = useGetCompany(selected);

  //const manager = company?.manager;
  //const managerName = `${company?.manager?.firstName} ${company?.manager?.lastName}`;
  //const managerEmail = company?.manager?.email;

  //const showManager = company && manager;
  //const showNotFound = company && !manager;

  return (
    <Dialog
      title="Contact"
      description="Need any help? View your manager's contact information by selecting an organization below."
      width={"500px"}
      content={
        <>
          <Select
            placeholder="Select organization"
            //options={companies}
            //getOptionLabel={(x) => x.name}
            //onChange={(x) => setSelected(x?.id)}
            //value={companies.find((x) => x.id === selected)}
          />

          <Box display={"flex"} alignItems={"center"} minHeight={"70px"}>
            {/*showManager && <ContactCard name={managerName} email={managerEmail} />*/}
            {/*showNotFound && <Text>Sorry! No manager found.</Text>*/}
          </Box>
        </>
      }
    >
      {/*companies.length > 0 && (
        <UserMenuButton icon={<Envelope size={24} />}>Contact</UserMenuButton>
      )*/}
    </Dialog>
  );
};

export default ContactButton;
