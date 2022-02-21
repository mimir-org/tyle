import { Dispatch } from "redux";
import { useEffect } from "react";
// import { LoginModule } from "../login";
import { LibraryModule } from "../library";
import { ErrorModule } from "../error";
import { ValidationModule } from "../validation";
import { TypeEditorComponent } from "../editor";
import {
  fetchLibrary,
  fetchLibraryInterfaceTypes,
  fetchLibraryTransportTypes,
} from "../../redux/store/library/librarySlice";
import { fetchBlobData } from "../../redux/store/editor/editorSlice";
import { fetchUser } from "../../redux/store/user/userSlice";

interface Props {
  dispatch: Dispatch;
}

/**
 * The main component for the Type Library.
 * @param interface
 * @returns all the modules and components in the Type Library application.
 */
const Home = ({ dispatch }: Props) => {
  useEffect(() => {
    dispatch(fetchLibraryInterfaceTypes());
    dispatch(fetchLibraryTransportTypes());
    dispatch(fetchLibrary(""));
    dispatch(fetchBlobData());
    dispatch(fetchUser());
  }, [dispatch]);

  return (
    <>
      {/* <LoginModule /> */}
      <LibraryModule />
      <TypeEditorComponent />
      <ErrorModule />
      <ValidationModule />
    </>
  );
};

export default Home;
