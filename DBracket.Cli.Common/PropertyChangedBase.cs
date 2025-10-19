using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace DBracket.Cli.Common;

/// <summary>Baseclass to notify the UI about changes in the ViewModel data</summary>
public abstract class PropertyChangedBase : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
{
    #region "----------------------------- Private Fields ------------------------------"
    /// <summary>Contains the validation errors of all properties</summary>
    [JsonIgnore]
    public readonly Dictionary<string, List<string>> _propertyErrors = new();
    #endregion



    #region "------------------------------ Constructor --------------------------------"

    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    /// <summary>Informs the UI that the data of a property is about to be changed, by triggering the event "PropertyChanging"</summary>
    /// <param name="propertyName">Name of the properties, whose data is about to be changed</param>
    protected void OnPropertyChanging(string propertyName)
    {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    /// <summary>Informs the UI that the data of a property is about to be changed, by triggering the event "PropertyChanging"</summary>
    /// <param name="callerMemberName">Name of the properties, whose data is about to be changed</param>
    protected void OnMySelfChanging([CallerMemberName] string callerMemberName = "")
    {
        if (!string.IsNullOrEmpty(callerMemberName))
            OnPropertyChanging(callerMemberName);
    }

    /// <summary>Informs the UI that the data of a property was changed, by triggering the event "PropertyChanged"</summary>
    /// <param name="propertyName">Name of the properties, whose data was changed</param>
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>Informs the UI that the data of a property was changed, by triggering the event "PropertyChanged"</summary>
    /// <param name="callerMemberName">Name of the properties, whose data was changed</param>
    protected void OnMySelfChanged([CallerMemberName] string callerMemberName = "")
    {
        if (!string.IsNullOrEmpty(callerMemberName))
            OnPropertyChanged(callerMemberName);
    }

    /// <summary>Gets all error descriptions of a property</summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>List of all the error descriptions</returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return new List<string>();

        if (_propertyErrors.ContainsKey(propertyName))
            return _propertyErrors.GetValueOrDefault(propertyName, new List<string>());

        return new List<string>();
    }

    /// <summary>Adds a validation error description to a property</summary>
    /// <param name="propertyName">Name of the property, whose input validation failed</param>
    /// <param name="errorDescription">Description of the error</param>
    public void AddError(string propertyName, string errorDescription)
    {
        if (!_propertyErrors.ContainsKey(propertyName))
            _propertyErrors.Add(propertyName, new List<string>());

        _propertyErrors[propertyName].Add(errorDescription);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    /// <summary>Resets all Errors of a property</summary>
    /// <param name="propertyName">Name of the property, whose errors should be reset</param>
    public void ResetErrors(string propertyName)
    {
        if (_propertyErrors.ContainsKey(propertyName))
            _propertyErrors[propertyName].Clear();
    }
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"
    /// <summary>Shows, if there are any properties whose input validation failed</summary>
    [JsonIgnore]
    public bool HasErrors => _propertyErrors.Any(x => x.Value.Any());
    #endregion

    #region "--------------------------------- Events ----------------------------------"
    /// <summary>Is triggered, before the data of a property is changed in the viewmodel</summary>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <summary>Is triggered, when a property in the viewmodel was changed</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Is triggered, when the validation of a property input failed</summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    #endregion
    #endregion
}